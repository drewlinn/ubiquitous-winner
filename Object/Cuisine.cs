using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace Restaurants
{
  public class Cuisine
  {
    private int _id;
    private string _name;
    private string _menu;

    public Cuisine(string name, string menu, int id = 0)
    {
      _id = id;
      _name = name;
      _menu = menu;
    }

    public int GetId()
    {
      return _id;
    }
    public string GetName()
    {
      return _name;
    }
    public void SetName(string newName)
    {
      _name = newName;
    }
    public string GetMenu()
    {
      return _menu;
    }
    public void SetMenu(string newMenu)
    {
      _menu = newMenu;
    }

    public override bool Equals(System.Object otherCuisine)
    {
      if(!(otherCuisine is Cuisine))
      {
        return false;
      }
      else
      {
        Cuisine newCuisine = (Cuisine) otherCuisine;
        bool idEquality = (this.GetId() == newCuisine.GetId());
        bool nameEquality = (this.GetName() == newCuisine.GetName());
        bool menuEquality = (this.GetMenu() == newCuisine.GetMenu());
        return (idEquality && nameEquality && menuEquality);
      }
    }

    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("DELETE FROM cuisine;", conn);
      cmd.ExecuteNonQuery();
      conn.Close();
    }

    public static List<Cuisine> GetAll()
    {
      List<Cuisine> AllCuisine = new List<Cuisine>{};

      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM cuisine;", conn);
      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int cuisineId = rdr.GetInt32(0);
        string cuisineName = rdr.GetString(1);
        string cuisineMenu = rdr.GetString(2);
        Cuisine newCuisine = new Cuisine(cuisineName, cuisineMenu, cuisineId);
        AllCuisine.Add(newCuisine);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return AllCuisine;
    }

    public void Save()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO cuisine (type, menu) OUTPUT INSERTED.id VALUES (@CuisineName, @CuisineMenu);", conn);

      SqlParameter nameParam = new SqlParameter("@cuisineName", this.GetName());
      SqlParameter menuParam = new SqlParameter("@CuisineMenu", this.GetMenu());
      cmd.Parameters.Add(nameParam);
      cmd.Parameters.Add(menuParam);
      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        this._id = rdr.GetInt32(0);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
    }

    // public static Cuisine Find(int id)
    // {
    //   return "string";
    // }
  }
}
