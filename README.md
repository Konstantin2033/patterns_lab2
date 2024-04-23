## Post service tracking system. Konstantine Tsurtsumia ZIPZ-22-1
The program emulates transportation of parcels with tracking via unique tracking-code.
For saving information used Sql database and class-adapter for acsessing tables.
In the program realised system of users through personal accounts, where several parcels can be linked to one user.

[Directory](Post/Classes) with classes

The **KISS** principle. All classes written in the same way, with initialization and get/set methods. All variable have clear and understandable names, every utilized element of the forms also has proper name with main form name mentioning.
[Check point class](Post/Classes/CheckPoint.cs)
[User class](Post/Classes/User.cs)
[Parcel class](Post/Classes/Parcel.cs)
[Database class]Post/Classes/DataBaseAdapter.cs)

**Yagni** stands for awoiding functions, that you don't need yet.
All function and variables in the project are utilized. There are no unnecessary methods or classes.

The **DRY** principle grounds on awoiding repeating parts of code in your project.
As example in the project there are static Warning class to show a message on screen. You can acsses this class at every point of the program, in every form and class.
With this class there is no need to call message.show() ever time.
[Warning class](Post/Classes/Warning.cs)
