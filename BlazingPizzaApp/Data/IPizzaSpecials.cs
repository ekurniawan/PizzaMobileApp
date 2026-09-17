using BlazingPizzaApp.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlazingPizzaApp.Data
{
    public interface IPizzaSpecials
    {
        List<PizzaSpecial> PizzaSpecials { get; }
    }
}
