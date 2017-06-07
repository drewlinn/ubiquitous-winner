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
      // Console.WriteLine(testCuisine.GetName() + " Cuisine test");
      testCuisine.Save();
      //Act
      Cuisine foundCuisine = Cuisine.Find(testCuisine.GetId());
      //Assert
      Assert.Equal(testCuisine, foundCuisine);
    }

    public void Dispose()
    {
      Restaurant.DeleteAll();
      Cuisine.DeleteAll();
    }

  }
}
