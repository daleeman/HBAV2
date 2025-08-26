Server=localhost\SQLEXPRESS;Database=master;Trusted_Connection=True;


broker@hba.com
Broker123!

seeker@hba.com
Seeker123!



# HBAV2
House Broker Application


"DefaultConnection": "Server=DESKTOP-UL3EADV\\SQLEXPRESS;Database=HBA;Trusted_Connection=True;TrustServerCertificate=True;"


broker@hba.com
Broker123!

seeker@hba.com
Seeker123!
--------------------------------------------------------------------------
Clean Architecture following Repository pattern

HBA.Domain 
HBA.Application 
HBA.Infrastructure 
HBA.Web 
--------------------------------------------------------------------------
Tables

dbo.Property 
PK Id (int)
FK PropertyTypeId (int)
PropertyName (nvarchar)
Location (nvarchar)
Price (decimal)

dbo.PropertyType
PK Id (int)
Type (nvarchar)

dbo.CommissionSetup
Pk Id (int)
FromAmount (decimal)
ToAmount (decimal)
CommissionValue (decimal)

Identity Tables for Authentication and Authorisation
