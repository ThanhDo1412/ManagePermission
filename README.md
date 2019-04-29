# ManagePermission

1. Description   
This is the demo project describe how to create a structure of users with permission and how to get, add, remove permission of each user.

2. Explain the time & space complexity of create struture of Users  


3. How to run   
Firstly, please run project ManagementPermission. After console displayed, input your user with role with format as below:
```
<NUMBER OF USERS>
<CEO's PERMISSIONS> 
<User1's PERMISSIONS> 
... 
<UserN's PERMISSIONS> 
<User1's Manager> 
... 
<UserN's Manager>
<Query1>
...
<QueryN>
```
For the Query, we just only accept 3 kinds of query: 
```
ADD <User number> <Permission>
REMOVE <User number> <Permission>
QUERY <User>
```
Explain for command:
ADD: add permission for user. This command will not have output
REMOVE: remove permission of user. This command will not have output
QUERY: get full permission of this user at current time
Finally, after finish our input, please type **Exist** or click **Enter** 2 times for finish your input.
Please look at this example as below for more understanding:
Example input:
```
6 
A F 
A B 
A C E 
A 
D 
A C 
A B 
CEO 
CEO 
1 
1 
1 
2 
ADD 2 X 
QUERY 2 
QUERY CEO 
REMOVE 2 X 
QUERY 2 
QUERY CEO
```
Example output:
```
A, B, C, D, E, F 
A, B, C, D 
A, B, C, E 
A 
D 
A, C A, B 
A, B, C, E, X 
A, B, C, D, E, F, X 
A, B, C, E 
A, B, C, D, E, F
```
