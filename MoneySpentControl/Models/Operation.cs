using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoneySpentControl.Models
{
    public class Operation
    {
        public DateTime DateTime { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public int Amount { get; set; }
        public bool IsProfit { get; set; }

        public Operation(string category, int amount, bool isProfit, string descriprion = null)
        {
            this.DateTime = DateTime.Now;
            this.Category = category;
            this.Description = descriprion;
            this.Amount = amount;
            this.IsProfit = isProfit;
        }

        public Operation(string category, int amount, int isProfit, string dateTime, string descriprion = null)
        {
            this.DateTime = DateTime.Parse(dateTime);
            this.Category = category;
            this.Description = descriprion;
            this.Amount = amount;
            this.IsProfit = isProfit == 1 ? true : false;
        }

        public override string ToString()
        {
            return $"Amount: {this.Amount}\nCategory: {this.Category}\nDesc: {this.Description}\n" +
                $"Is Profit?: {this.IsProfit}\n{this.DateTime}\n\n";
        }
    }
}
