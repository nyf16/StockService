using System;
using System.Collections.Generic;
using System.Text;
using StockService.Domain.Entity;

namespace StockService.Domain.Product
{
    public class Product : Entity<Guid>
    {
        public string UrunAdi { get; set; }
        public string UrunKodu { get; set; }
        public string Kompozisyon { get; set; }
        public string Renk { get; set; }
        public string Kalite { get; set; }
        public int Miktar { get; set; }
        public string RezerveDurum { get; set; }
        public string Musteri { get; set; }
    }
}