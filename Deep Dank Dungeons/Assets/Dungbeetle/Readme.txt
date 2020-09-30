
*---------------------------------------*
|                                       |
|            Dungbeetle                 |
|            ----------                 |
|       Get your shit straight          |
|                                       |
*---------------------------------------*


A message from the author:
  I recommend you go visit the documentation at:
    'Help -> Dungbeetle Documentation'
  or
    Read this file, for a fully text-based introduction.


=== Introduction ===

Hi there!

Dungbeetle is a complete system for encouraging and catching user feedback in
Unity projects.  It comes with a bug reporting gui, developer gui and server.

The server keeps all the bug reports in one place, and the developer gui acts
as the only way to interact with that set of bug reports.


=== Getting Started ===

First time?

    Put a 'BugReportFormLauncher' on a GameObject. You're ready to go!

Want your own gui?

    Modify or replace 'BugReportForm'. Easy peasy!

Want to automate reporting?

    Call 'Dungbeetle.BugReportSender.Send(...)'.

=== The Server ===

The Dungbeetle server is a simplistic, but flexible piece of software. It can
run inside the Unity editor, in a demo-like way, or it can run as a separate
mono executable on any operating system that can run mono. Windows, in
particular, does this natively, as if the executable was native.

If you have a long-term project... I recommend that you compile the server and
run it 24/7, so that you don't loose any user submitted bug reports.

Find your way to:
 'Window->Dungbeetle->Tools->Build DLL or Server->Standalone Server'

Then turn off the in-editor server from:
  'Window->Dungbeetle->Connection'
 or
  'Window->Dungbeetle->Tools->In-Editor Server Log'


=== The Developer GUI ===

The developer GUI is only available inside your Unity project. To open the
overview interface, go to 'Window->Dungbeetle->Bug Reports'.

It has a tabbed-like window with the categories:
 | All Open | Unassigned | Assigned | Fixed | Resolved |

These tabs represent different subsets of bugs that you might want to view or
edit. In Dungbeetle the lifespan of a bug is as follows:

  Unassigned  -->  Assigned  -->  Fixed  -->  Resolved

For each step on the way, you need to go to the respective tab to find it.

If you want a to decline a bug, you can delete the bug entirely, or you can
change its priority to 'Declined'.


=== Documentation ===

Open 'Help -> Dungbeetle Documentation'
  or
Take a look at 'Assets/Dungbeetle/.Documentation/introduction.htm' in your web browser.


=== Final Notes ===

If you have any comments, or need help, don't hesitate to contact me at:
  post@fredrik.ludvigsen.name


Enjoy seamless bug reporting and tracking in Unity!

