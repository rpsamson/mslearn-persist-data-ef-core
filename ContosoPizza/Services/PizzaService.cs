using ContosoPizza.Data;
using ContosoPizza.Models;
using Microsoft.EntityFrameworkCore;

namespace ContosoPizza.Services;

public class PizzaService
{
    private readonly PizzaContext _context;
    public PizzaService(PizzaContext context)
    {
        _context = context;
    }

    public IEnumerable<Pizza> GetAll()
    {
        return _context.Pizzas.AsNoTracking().ToList();
        
    }

    public Pizza? GetById(int id)
    {
        return _context.Pizzas
                        .Include(p => p.Toppings)
                        .Include(p => p.Sauce)
                        .AsNoTracking()
                        .SingleOrDefault(p => p.Id == id);
    }

    public Pizza? Create(Pizza newPizza)
    {
        _context.Add(newPizza);
        _context.SaveChanges();
        return newPizza;

    }

    public void AddTopping(int PizzaId, int ToppingId)
    {
        var pizzaToUpdate = _context.Pizzas.Find(PizzaId);
        var toppingToAdd  = _context.Toppings.Find(ToppingId);
        
       if ((pizzaToUpdate is null) || (toppingToAdd is null))
       {
         throw new InvalidOperationException("Pizza or Topping does not exist");
       }

       pizzaToUpdate.Toppings ??= new List<Topping>();
       pizzaToUpdate.Toppings.Add(toppingToAdd);

       _context.SaveChanges();

    }

    public void UpdateSauce(int PizzaId, int SauceId)
    {
       var pizzaToUpdate = _context.Pizzas.Find(PizzaId);
       var sauceToUpdate = _context.Sauces.Find(SauceId);

       if ((pizzaToUpdate is null) || (sauceToUpdate is null))
       {
         throw new InvalidOperationException("Pizza or Sauce does not exist");
       }
       pizzaToUpdate.Sauce = sauceToUpdate;
       _context.SaveChanges();

    }

    public void DeleteById(int id)
    {
        var pizzaToDelete = _context.Pizzas.Find(id);
        if (pizzaToDelete is not null) 
        {
            _context.Remove(pizzaToDelete);
            _context.SaveChanges();
        }
    }
}