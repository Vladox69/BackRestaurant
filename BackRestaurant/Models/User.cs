﻿namespace BackRestaurant.Models
{
    public class User
    {
        public int? id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string role { get; set; }
        public int? boss_id { get; set; }
    }
}
