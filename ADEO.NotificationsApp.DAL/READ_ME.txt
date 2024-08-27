Please run below command in Package manager console, if migrations are not present or fresh migration needs to be created.

Please also select "ADEO.NotificationsApp.DAL" as Default project in Package manager console 

		Add-Migration initialDatabaseCreation
		update-database
		 