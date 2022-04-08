﻿namespace EF_Core_Pagination
{
    public class ProductResponse
    {
        public List<Product> Products { get; set; } = new List<Product>();

        public int Pages { get; set; }

        public int CurrentPage { get; set; }
    }
}
