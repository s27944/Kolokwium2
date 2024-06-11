using Kolokwium.Contexts;
using Kolokwium.Exceptions;
using Kolokwium.Models;
using Kolokwium.RequestModels;
using Kolokwium.ResponseModels;
using Microsoft.EntityFrameworkCore;

namespace Kolokwium.Services;

public interface IDatabaseService
{
    public Task<Response> Get(int id);
    public Task<PostResponse> Post(int id, Request request);
}

public class DatabaseService : IDatabaseService
{
    private readonly DatabaseContext _context;

    public DatabaseService(DatabaseContext context)
    {
        _context = context;
    }


    public async Task<Response> Get(int id)
    {
        var characterExists = await _context.Characters.FirstOrDefaultAsync(c => c.PK == id);
        if (characterExists == null)
        {
            throw new NotFoundException($"Character o ID {id} nie istnieje");
        }
        
        var response = _context.Characters
            .Where(c => c.PK == id)
            .Select(c => new Response
            {
                firstName = c.firstName,
                lastName = c.last_name,
                currentWeight = c.current_weig,
                maxWeight = c.max_weight,
                money = c.money,
                backpack = c.Backpack_Slots
                    .Select(bs => new Response.Backpack
                    {
                        slotId = bs.PK,
                        itemName = bs.Items.name,
                        itemWeight = bs.Items.weig
                    })
                    .ToList(),
                tytuly = c.Character_Titles
                    .Select(ct => new Response.Tytuly
                    {
                        title = ct.Titles.name,
                        aquireAt = ct.aquire_at
                    })
                    .ToList()
            })
            .FirstOrDefault();

        return response;
    }
    

    public async Task<PostResponse> Post(int idCharacter, Request request)
    {
        using (var transaction = await _context.Database.BeginTransactionAsync())
        {
            try
            {
                
                //sprawdzenie czy istnieją itemy
                foreach (var id in request.items)
                {
                    var itemExists = await _context.Items.FirstOrDefaultAsync(i => i.PK == id);
                    if (itemExists == null)
                    {
                        throw new NotFoundException($"Item od id {id} nie istnieje!");
                    }
                }

                
                //zsumowanie wag itemów
                int sumaWagPrzedmiotow = 0;
                foreach (var id in request.items)
                {
                    var item = await _context.Items.FirstOrDefaultAsync(i => i.PK == id);
                    sumaWagPrzedmiotow += item.weig;
                }

                //sprawdzenie wagi bohatera po dodaniu przedmiotów
                var character = await _context.Characters.FirstOrDefaultAsync(c => c.PK == idCharacter);
                int obecnaWaga = character.current_weig;
                int maxWaga = character.max_weight;
                int wagaPoDodaniuPrzedmiotow = obecnaWaga + sumaWagPrzedmiotow;


                if (wagaPoDodaniuPrzedmiotow > maxWaga)
                {
                    throw new PlayerOverLoadedException(
                        $"Character o ID {idCharacter} nie może unieść wszystkich przedmiotów");
                }

                
                //dodanie itemów do bazy danych i pobranie nowych idSlotów
                List<int> slotIds = new List<int>();
                
                foreach (var id in request.items)
                {
                    var itemNaSlocie = new Backpack_Slots
                    {
                        itemID = id,
                        characterID = idCharacter
                    };
                    _context.Backpack_Slots.Add(itemNaSlocie);
                    await _context.SaveChangesAsync();

                    slotIds.Add(itemNaSlocie.PK);
                }

                await _context.SaveChangesAsync();
                
                //aktualizacja wagi charactera
                var charac = await _context.Characters.FirstOrDefaultAsync(c => c.PK == idCharacter);
               
                charac.current_weig = wagaPoDodaniuPrzedmiotow;
                await _context.SaveChangesAsync();

                var response = new PostResponse();

                int i = 0;
                foreach (var id in request.items)
                {
                    var dane = new PostResponse.Dane
                    {
                        slotId = slotIds[i],
                        itemId = id,
                        characterId = idCharacter
                    };
                    
                    response.data.Add(dane);
                    
                    i++;
                }
                
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                
                return response;
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
        
    }
}