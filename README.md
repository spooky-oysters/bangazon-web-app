# Bangazon Web App
## Authors: Dre Randici, John Dulaney, Kimberly Bird, Krys Mathis.
# About
This Web application is built from the ground up using Microsoft's MVC ASP.net framework. It draws data from a SQL database that we here at Bangazon constructed. Once the user is registered, this Web Application allows the user to view various sorts of data on products, add a product to sell (including all functionality reguarding deleting and editing), and add products to a shopping cart for purchase. 

# Steps to Install
This Application requires solid grasp of using the command line on your computer. Further learning on navigating your Command Shell here: [Dosprompt Basics](http://dosprompt.info/basics.asp)

1. First you must download a copy of Virtual Studio Community Edition: [Visual Studio Official Download](https://www.visualstudio.com/downloads/)
1. After installing VS, we now need to clone the repository down to your local machine. First ensure you have Git installed - [Download here](http://git-scm.com/download/win) 
Navigate to the desired download folder then use this command:
```
git clone git@github.com:spooky-oysters/bangazon-web-app.git
```
This initialized git and downloaded the Bangazon MVC Web applications files to your computer. We are now ready to work with VS.
1. Either open `bangazon-web-app.sln` from VS or simply navigate to it (type `explorer .` in CMD while in the directory) with explorer and double click the solution (.sln) file. 
1. At the Upper right of you Visual Studio there is a 'Quick Launch' input. Type `package manager console` into it and select the first result.
1. A new window should pop up, thats the console. Execute the following commands (descriptive label is a placeholder):
```
add-migration descriptiveLabel
```
then:
```
update-database
```
1. Now find the green arrow in the top toolbar, if it does not say Bangazon, click the down arrow on the right and select Bangazon. 
1. Click the Green arrow. This will open a CMD window and a web browser. Be prepared to wait a few seconds depending on the powe of your computer.
1. Voilà! You are now interacting with the Bangazon Web Application. See the features section for the functionality our application is capable of.
 
# Features

Coming Soon!
