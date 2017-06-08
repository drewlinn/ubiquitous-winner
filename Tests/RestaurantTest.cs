using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Restaurants
{
  [Collection("Restaurants")]
  public class RestaurantTest : IDisposable
  {
    public RestaurantTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb; Initial Catalog=restaurants_test; Integrated Security=SSPI;";
    }
    [Fact]
    public void Test_DatabaseEmptyAtFirst()
    {
      //Arrange, Act
      int result = Restaurant.GetAll().Count;

      //Assert
      Assert.Equal(0, result);

    }

    [Fact]
    public void Test_Save_SavesToDatabase()
    {
      //Arrange
      Restaurant testRestaurant = new Restaurant("Shoney", "American", 1);

      //Act
      testRestaurant.Save();
      List<Restaurant> result = Restaurant.GetAll();
      List<Restaurant> testList = new List<Restaurant>{testRestaurant};

      //Assert
      Assert.Equal(testList, result);
    }

    [Fact]
    public void Test_Find_FindRestaurantInDatabase()
    {
      //Arrange
      Restaurant testRestaurant = new Restaurant("Shoney's", "American", 1);
      testRestaurant.Save();

      //Act
      Restaurant foundRestaurant = Restaurant.Find(testRestaurant.GetId());

      //Assert
      Assert.Equal(testRestaurant, foundRestaurant);

    }

    [Fact]
    public void Test_Update_UpdatesRestaurantInDatabase()
    {
      //Arrange
      string style = "Cafe, baby";
      string name = "Baby Cafe";
      Restaurant testaurant = new Restaurant(name, style, 1);
      testaurant.Save();
      string newStyle = "Fine Dining";
      string newName = "Dine Fining";
      //Act
      testaurant.Update("Fine Dining", "Dine Fining");
      string result = testaurant.GetStyle();

      //Assert
      Assert.Equal(newStyle, result);
    }

    public void Dispose()
    {
      Restaurant.DeleteAll();
      Cuisine.DeleteAll();
    }
  }
}
