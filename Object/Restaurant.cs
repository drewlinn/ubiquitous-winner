using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Restaurants
{
  public class Restaurant
  {
    private int _id;
    private string _name;
    private string _style;
    private int _cuisine_id;

    public Restaurant(string name, string style, int cuisine_id, int id = 0)
    {
      _name = name;
      _style = style;
      _cuisine_id = cuisine_id;
      _id = id;
    }

    public override bool Equals(System.Object otherRestaurant)
    {
      if(!(otherRestaurant is Restaurant))
      {
        return false;
      }
      else
      {
        Restaurant newRestaurant = (Restaurant) otherRestaurant;
        bool idEquality = (this.GetId() == newRestaurant.GetId());
        bool nameEquality = (this.GetName() == newRestaurant.GetName());
        bool styleEquality = (this.GetStyle() == newRestaurant.GetStyle());
        bool cuisine_idEquality = (this.GetCuisineId() == newRestaurant.GetCuisineId());
        return (idEquality && nameEquality && styleEquality && cuisine_idEquality);
      }
    }
    public int GetId()
    {
      return _id;
    }
    // public void SetId(int id)
    // {
    //   _id = id;
    // }
    public string GetName()
    {
      return _name;
    }
    // public void SetName(string name)
    // {
    //   _name = name;
    // }
    public string GetStyle()
    {
      return _style;
    }
    // public void SetStyle(string style)
    // {
    //   _style = style;
    // }
    public int GetCuisineId()
    {
      return _cuisine_id;
    }
    // public void SetCuisineId(int cuisine_id)
    // {
    //   _cuisine_id = cuisine_id;
    // }

    public static List<Restaurant> GetAll()
    {
      List<Restaurant> AllRestaurants = new List<Restaurant>{};

      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM restaurant", conn);
      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int id = rdr.GetInt32(0);
        string name = rdr.GetString(1);
        string style = rdr.GetString(2);
        int cuisine_id = rdr.GetInt32(3);
        Restaurant newRestaurant = new Restaurant(name, style, cuisine_id, id);
        AllRestaurants.Add(newRestaurant);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return AllRestaurants;
    }
    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("DELETE FROM restaurant;", conn);
      cmd.ExecuteNonQuery();
      conn.Close();
    }

    public void Save()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO restaurant (name, style, cuisine_id) OUTPUT INSERTED.id VALUES (@name, @style, @cuisine_id);", conn);

      SqlParameter nameParam = new SqlParameter("@name", this.GetName());

      SqlParameter styleParam = new SqlParameter("@style", this.GetStyle());

      SqlParameter cuisine_idParam = new SqlParameter("@cuisine_id", this.GetCuisineId());

      cmd.Parameters.Add(nameParam);
      cmd.Parameters.Add(styleParam);
      cmd.Parameters.Add(cuisine_idParam);

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

    public static Restaurant Find(int id)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM restaurant WHERE id = @id;", conn);
      SqlParameter idParameter = new SqlParameter("@id", id.ToString());

      cmd.Parameters.Add(idParameter);
      SqlDataReader rdr = cmd.ExecuteReader();

      int foundId = 0;
      string name = null;
      string style = null;
      int cuisine_id = 0;
      while(rdr.Read())
      {
        foundId = rdr.GetInt32(0);
        name = rdr.GetString(1);
        style = rdr.GetString(2);
        cuisine_id = rdr.GetInt32(3);
      }
      Restaurant foundRestaurant = new Restaurant(name, style, cuisine_id, foundId);
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return foundRestaurant;
    }
  }
}
