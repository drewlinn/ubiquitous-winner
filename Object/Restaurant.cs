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

    public class Restaurant(string name, string style, int cuisine_id, int id = 0)
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
    public void SetId(int id)
    {
      _id = id;
    }
    public string GetName()
    {
      return _name;
    }
    public void SetName(string name)
    {
      _name = name;
    }
    public string GetStyle()
    {
      return _style;
    }
    public void SetStyle(string style)
    {
      _style = style;
    }
    public int GetCuisineId()
    {
      return _cuisine_id;
    }
    public void SetCuisineId(int cuisine_id)
    {
      _cuisine_id = cuisine_id;
    }
  }
}
