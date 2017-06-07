using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Restaurants
{
  public class CuisineTest : IDisposable
  {
    public CuisineTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb; Initial Catalog=restaurants_test; Integrated Security=SSPI;";
    }

    [Fact]
    public void Test_CuisineEmptyAtFirst()
    {
      //Arrange,Act
      int result = Cuisine.GetAll().Count;

      //Assert
      Assert.Equal(0, result);
    }

    [Fact]
    public void Test_Save_SaveCuisineToDatabase()
    {
      //Arrange
      Cuisine testCuisine = new Cuisine("American", "bacon");
      testCuisine.Save();

      //Act
      List<Cuisine> result = Cuisine.GetAll();
      List<Cuisine> testList = new List<Cuisine>{testCuisine};

      //Assert
      Assert.Equal(testList, result);
    }

    [Fact]
    public void Test_Find_FindsCuisineInDatabase()
    {
      //Arrange
      Cuisine testCuisine = new Cuisine("Italian", "Pizza");
      testCuisine.Save();
      //Act
      Cuisine foundCuisine = Cuisine.Find(testCuisine.GetId());
      //Assert
      Assert.Equal(testCuisine, foundCuisine);
    }

    [Fact]
    public void Test_GetRestaurants_RetrieveAllRestaurantsWithinCuisine()
    {
      //Arrange
      Cuisine testCuisine = new Cuisine("Italian", "pizza");
      testCuisine.Save();

      Restaurant firstRestaurant = new Restaurant("Shoney's", "American", testCuisine.GetId());
      firstRestaurant.Save();
      Restaurant secondRestaurant = new Restaurant("Luis's", "Italian", testCuisine.GetId());
      secondRestaurant.Save();

      //Act
      List<Restaurant> testRestaurantList = new List<Restaurant>{firstRestaurant, secondRestaurant};
      List<Restaurant> resultRestaurantList = testCuisine.GetRestaurants();

      //Assert
      Assert.Equal(testRestaurantList, resultRestaurantList);


    }

    public void Dispose()
    {
      
      Cuisine.DeleteAll();
    }

  }
}
