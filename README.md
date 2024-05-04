## Post service tracking system. Konstantine Tsurtsumia ZIPZ-22-1
The program emulates transportation of parcels with tracking via unique tracking-code.
For saving information used Sql database and class-adapter for acsessing tables.
In the program realised system of users through personal accounts, where several parcels can be linked to one user.

**Process**
You start with main window, where you can write down your tracing code and find your parcel, or authorize to your account and check all your parcels. If you dont have accoun you can create one. In account menu you can create new parcels or delete old ones. Also you canpick any of your parcles and check detail info about sender, reciver, time and type of parcel. Also there are checkpoint with date and tine to know location of your parcel.
Without authorisation you can find a parcel by tracking code, but you wont be allowed do delete it.

[Directory](Post/Classes) with classes

The **KISS** principle. All classes written in the same way, with initialization and get/set methods. All variable have clear and understandable names, every utilized element of the forms also has proper name with main form name mentioning.
[Check point class](Post/Classes/CheckPoint.cs)
[User class](Post/Classes/User.cs)
[Parcel class](Post/Classes/Parcel.cs)
[Database class](Post/Classes/DataBaseAdapter.cs)

**Yagni** stands for awoiding functions, that you don't need yet.
All function and variables in the project are utilized. There are no unnecessary methods or classes.

The **DRY** principle grounds on awoiding repeating parts of code in your project.
As example in the project there are static Warning class to show a message on screen. You can acsses this class at every point of the program, in every form and class.
With this class there is no need to call message.show() ever time.
[Warning class](Post/Classes/Warning.cs)


**Used design patterns**

**Mediator pattern**
To check all the input boxes in Login window used a mediator patter with class [RegistrationMediator](Post/Classes/RegistrationMediator.cs)
This class checks input field to enable "continue" button.

**Adapter pattern**
In the project you can find a adapter for SQL database, that has no suitable interface in C# to work with. Basic functional methods are used to access tables and even each rows. The database adapter can be easly used as a template for other projects with minor changes in table data. 
[Database class](Post/Classes/DataBaseAdapter.cs)
