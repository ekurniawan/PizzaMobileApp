using BlazingPizzaApp.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlazingPizzaApp.Services
{
    public class OrderState
    {
        public bool ShowingConfigureDialog { get; private set; }
        public Pizza ConfiguringPizza { get; private set; }
        public Order Order { get; private set; } = new Order();
    }
}
