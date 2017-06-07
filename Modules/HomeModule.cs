using System.Collections.Generic;
using Nancy;
using Nancy.ViewEngines.Razor;

namespace Restaurants
{
  public class HomeModule : NancyModule
  {
    public HomeModule()
    {
      Get["/"] = _ =>{
        return View["index.cshtml"];
      };
      Get["/restaurants"] = _ =>{
        List<Restaurant> AllRestaurants = Restaurant.GetAll();
        return View["restaurants.cshtml", AllRestaurants];
      };
      Get["/cuisines"] = _ => {
        List<Cuisine> AllCuisine = Cuisine.GetAll();
        return View["cuisines.cshtml", AllCuisine];
      };
      Get["/restaurant/new"] = _ =>  {
        List<Cuisine> AllCuisine = Cuisine.GetAll();
        return View["/add_restaurant.cshtml", AllCuisine];
      };
      Get["/cuisine/new"] = _ =>  {
        return View["/add_cuisine.cshtml"];
      };
      Post["/restaurants"]= _ =>{
        Restaurant newRestaurant = new Restaurant(Request.Form["restaurant"], Request.Form["style"], Request.Form["cuisine_id"]);
        newRestaurant.Save();
        return View["restaurant_added.cshtml", newRestaurant];
      };
      Post["/cuisines"]= _ =>{
        Cuisine newCuisine = new Cuisine(Request.Form["cuisine-type"], Request.Form["menu"]);
        newCuisine.Save();
        return View["cuisine_added.cshtml", newCuisine];
      };
    }
  }
}
