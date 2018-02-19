insert into dbo.AspNetUsers
(
id, 
AccessFailedCount, 
ConcurrencyStamp, 
Email, 
EmailConfirmed, 
LockoutEnabled, 
LockoutEnd, 
NormalizedEmail, 
NormalizedUserName,
PasswordHash, 
PhoneNumber, 
PhoneNumberConfirmed, 
SecurityStamp, 
TwoFactorEnabled, 
UserName
)
values(0, null, null, 'user_0@gmail.com', 1, 0, null, null, null, null, 1234567891, 1, null, 0, 'user_0');