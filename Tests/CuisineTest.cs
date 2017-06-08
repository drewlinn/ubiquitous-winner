using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Restaurants
{
  [Collection("Restaurants")]
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

    [Fact]
    public void Test_Update_UpdatesCuisineInDatabase()
    {
      //Arrange
      string menu = "burger";
      Cuisine testCuisine = new Cuisine("italian", menu);
      testCuisine.Save();
      string newMenu = "salad";

      //Act
      testCuisine.Update("salad");
      string result = testCuisine.GetMenu();

      //Assert
      Assert.Equal(newMenu, result);
    }

    [Fact]
    public void Test_Delete_DeleteCuisineFromDatabase()
    {
      //Arrange
      string name1 = "Gary";
      Cuisine testCategory1 = new Cuisine(name1, "menu1");
      testCategory1.Save();

      string name2 = "Wallace";
      Cuisine testCategory2 = new Cuisine(name2, "menu2");
      testCategory2.Save();

      Restaurant testaurant = new Restaurant("Shoney's", "fastfood", testCategory1.GetId());
      testaurant.Save();
      Restaurant testaurant1 = new Restaurant("Steven Luicci", "trap house", testCategory2.GetId());
      testaurant1.Save();
      //Act
      testCategory1.Delete();
      List<Cuisine> resultCuisine = Cuisine.GetAll();
      List<Cuisine> testCuisine = new List<Cuisine> {testCategory2};

      List<Restaurant> resultRestaurants = Restaurant.GetAll();
      List<Restaurant> testaurantList = new List<Restaurant> {testaurant1};
      //Assert
      Assert.Equal(testCuisine, resultCuisine);
      Assert.Equal(testaurantList, resultRestaurants);
    }

    public void Dispose()
    {
      Restaurant.DeleteAll();
      Cuisine.DeleteAll();
    }

  }
}
