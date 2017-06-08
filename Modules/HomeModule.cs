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
        Dictionary<string, object> model = new Dictionary<string, object>();
        Restaurant newRestaurant = new Restaurant(Request.Form["restaurant"], Request.Form["style"], Request.Form["cuisine_id"]);
        var selectedCuisine = Cuisine.Find(newRestaurant.GetCuisineId());
        newRestaurant.Save();
        model.Add("cuisine", selectedCuisine);
        model.Add("restaurant", newRestaurant);
        return View["restaurant_added.cshtml", model];
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
      Patch["/cuisine/edit/{id}"] = parameters =>{
        Cuisine SelectedCuisine = Cuisine.Find(parameters.id);
        SelectedCuisine.Update(Request.Form["cuisine-menu"]);
        return View["success.cshtml"];
      };
      Get["/restaurant/edit/{id}"] = parameters => {
        Restaurant selectedRestaurant = Restaurant.Find(parameters.id);
        return View["restaurant_edit.cshtml", selectedRestaurant];
      };
      Patch["/restaurant/edit/{id}"] = parameters =>{
        Restaurant SelectedRestaurant = Restaurant.Find(parameters.id);
        SelectedRestaurant.Update(Request.Form["restaurant-name"], Request.Form["restaurant-style"]);
        return View["success.cshtml"];
      };
      Get["cuisine/delete/{id}"] = parameters => {
        Cuisine SelectedCuisine = Cuisine.Find(parameters.id);
        return View["cuisine_delete.cshtml", SelectedCuisine];
      };
      Delete["cuisine/delete/{id}"] = parameters => {
        Cuisine SelectedCuisine = Cuisine.Find(parameters.id);
        SelectedCuisine.Delete();
        return View["success.cshtml"];
      };
      Get["restaurant/delete/{id}"] = parameters => {
        Restaurant SelectedRestaurant = Restaurant.Find(parameters.id);
        return View["restaurant_delete.cshtml", SelectedRestaurant];
      };
      Delete["restaurant/delete/{id}"] = parameters => {
        Restaurant SelectedRestaurant = Restaurant.Find(parameters.id);
        SelectedRestaurant.Delete();
        return View["success.cshtml"];
      };
    }
  }
}
