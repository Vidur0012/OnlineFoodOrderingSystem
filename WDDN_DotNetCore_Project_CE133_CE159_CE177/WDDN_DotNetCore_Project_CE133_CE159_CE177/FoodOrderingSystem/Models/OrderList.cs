using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace FoodOrderingSystem.Models
{
    public partial class OrderList
    {
        public int Id { get; set; }
        public string Item { get; set; }
        public int Quantity { get; set; }
        public string TotalPrice { get; set; }

        //internal int SaveDetails()
        //{
        //    //var cs= @"server=DESKTOP-FMD1EAK\SQLEXPRESS; database=Menu; trusted_connection=true;";
        //    var cs = @"server=(localdb)\\MSSQLLocalDB;database=FoodOrderingSysDB;Trusted_Connection=true";
        //    SqlConnection con = new SqlConnection(cs);
        //    string query = "INSERT INTO OrderList(Item, Quantity, TotalPrice) values ('" + Item + "','" + Quantity + "','" + (Quantity * Convert.ToInt32(TotalPrice)).ToString() + "')";
        //    SqlCommand cmd = new SqlCommand(query, con);
        //    con.Open();
        //    int i = cmd.ExecuteNonQuery();
        //    con.Close();
        //    return i;
        //}
    }
}
