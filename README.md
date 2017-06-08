# Restaurants

#### A program that allows users to create restaurants 6/7/17

#### By **Kimlan Nguyen and Andrew Dalton**

## Description

A website created with C# and HTML where a user can create a list of restaurants and the type of cuisine that they offer.


### Specs
| Spec | Input | Output |
| :-------------     | :------------- | :------------- |
| **User can create a restaurant** | add Shoney's  | Shoney's |
| **A restaurant can only have one cuisine**| Shoney's | name: Shoney's cuisine: American |
| **User can read about the added restaurants** | Shoney's| Name: "Shoney's" cuisine: American |
| **User can update the restaurants they created**| Shoney's | Name: "Shoney's" cuisine: Mexican |
| **User can delete the restaurants that have been created**| Shoney's | Delete: Shoney's |
| **User can search for all the restaurant that share the same cuisine** | Search: Mexican | Output: Shoney's, Luis's |


## Setup/Installation Requirements

1. To run this program, you must have a C# compiler. I use [Mono](http://www.mono-project.com).
2. Install the [Nancy](http://nancyfx.org/) framework to use the view engine. Follow the link for installation instructions.
3. Clone this repository.
4. Open the command line--I use PowerShell--and navigate into the repository. Use the command "dnx kestrel" to start the server.
5. On your browser, navigate to "localhost:5004" and enjoy!

## Known Bugs
* No known bugs at this time.

## Technologies Used
* C#
  * Nancy framework
  * Razor View Engine
  * ASP.NET Kestrel HTTP server
  * xUnit

* HTML

## Support and contact details

_Email no one with any questions, comments, or concerns._

### License

*{This software is licensed under the MIT license}*

Copyright (c) 2017 **_{Kimlan Nguyen and Andrew Dalton}_**
