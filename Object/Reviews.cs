using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Restaurants
{
  public class Reviews
  {
    private string _userName;
    private string _comment;
    private int _id;
    private int _restaurant_id;

    public Reviews(string userName, string comment, int restaurant_id, int id = 0)
    {
      _userName = userName;
      _comment = comment;
      _id = id;
      _restaurant_id = restaurant_id;
    }

    public override bool Equals(System.Object otherReviews)
    {
      if(!(otherReviews is Reviews))
      {
        return false;
      }
      else
      {
        Reviews newReviews = (Reviews) otherReviews;
        bool idEquality = (this.GetId() == newReviews.GetId());
        bool commentEquality = (this.GetComment() == newReviews.GetComment());
        bool restaurant_idEquality = (this.GetRestaurantId() == newReviews.GetRestaurantId());
        return (idEquality && commentEquality && restaurant_idEquality);
      }
    }

    public int GetId()
    {
      return _id;
    }
    public string GetComment()
    {
      return _comment;
    }
    public int GetRestaurantId()
    {
      return _restaurant_id;
    }

  }
}
