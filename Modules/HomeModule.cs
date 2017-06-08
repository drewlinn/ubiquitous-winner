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
      Get["/restaurants/{id}"] = parameters => {
        Dictionary<string, object> model = new Dictionary<string, object>();
        var selectedRestaurant = Restaurant.Find(parameters.id);
        var selectedCuisine = Cuisine.Find(selectedRestaurant.GetCuisineId());
        model.Add("cuisine", selectedCuisine);
        model.Add("restaurant", selectedRestaurant);
        return View["restaurant.cshtml", model];
      };
      Get["/cuisines/{id}"]= parameters => {
        Dictionary<string, object> model = new Dictionary<string, object>();
        var selectedCuisine = Cuisine.Find(parameters.id);
        var RestaurantsCuisine = selectedCuisine.GetRestaurants();
        model.Add("cuisine", selectedCuisine);
        model.Add("restaurants", RestaurantsCuisine);
        return View["cuisine.cshtml", model];
      };
      Post["/cuisines/cleared"] = _ =>{
        Restaurant.DeleteAll();
        Cuisine.DeleteAll();
        return View["cleared.cshtml"];
      };
      Post["/restaurants/cleared"] = _ =>{
        Restaurant.DeleteAll();
        return View["cleared.cshtml"];
      };
      Get["/cuisine/edit/{id}"] = parameters => {
        Cuisine SelectedCuisine = Cuisine.Find(parameters.id);
        return View["cuisine_edit.cshtml", SelectedCuisine];
      };
      // Post["/cuisine/edit/{id}"] = parameters => {
      //   Cuisine SelectedCuisine =
      // };

      Get["/restaurant/edit/{id}"] = parameters => {
        Restaurant selectedRestaurant = Restaurant.Find(parameters.id);
        return View["restaurant_edit.cshtml", selectedRestaurant];
      };
    }
  }
}
