﻿using ConstradeApi.Entity;

namespace ConstradeApi.Model.MProduct
{
    public class FavoriteModel
    {
        public int FavoriteId { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public DateTime Date { get; set; }

    }
}
