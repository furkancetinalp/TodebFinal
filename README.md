# TODEB Bootcamp Final Project - Apartment System
## The application aims to manage the monthly fee and common use electricity, water and natural gas bills of the apartments through a system.

### At first, the database is empty. In the User section an admin account should be initialized. So, first user must be admin and the program will check it.

## In order to add an admin, a house should exist in the database. This is the reason why house and admin are created at the same time. That is the only exceptional situation for this application. When admin is created, a random house automaticallly created and its house number is set to admin's House Number. Admin then can update house.

Identityno => 4 digits, Phone => 11 digits and must begin with 05 
After entering inputs, the program will create a 4 digits PASSWORD automatically and show it as an output.
![Ekran Görüntüsü (294)](https://user-images.githubusercontent.com/99509540/184578155-9a24dc84-0d1c-4042-bf03-0cf76ab68148.png)

After getting password, we can get a login by entering email and given password. Then we can get a token.
![Ekran Görüntüsü (296)](https://user-images.githubusercontent.com/99509540/184578878-091b1eed-ba93-41fc-9a61-5b636afd960a.png)

After authorization as admin, we can have an access for methods. We can update the house which has been created automatically.
![Ekran Görüntüsü (298)](https://user-images.githubusercontent.com/99509540/184579299-32168ce3-cba2-4736-b276-850a5fe2e347.png)

Inserting a house 
![Ekran Görüntüsü (300)](https://user-images.githubusercontent.com/99509540/184579595-5e015111-1498-4bac-bc24-869e569c6e3b.png)

Inserting a user and getting auto created password
![Ekran Görüntüsü (302)](https://user-images.githubusercontent.com/99509540/184580012-30b48503-f67f-4dbf-a186-bae8d55a35bb.png)

Inserting a bill --- For type; 1=>Feee : 2=>Electricity : 3=>Water : 4=>Gas ---
Also, we can only add a bill for either current month  or previous month
![Ekran Görüntüsü (304)](https://user-images.githubusercontent.com/99509540/184580573-da99f457-9e95-47fc-a299-726783dadc4e.png)

