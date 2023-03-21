# HangfireIntegration

First you need to install the following packages:
1- Hangfire.Core
2- Hangfire.AspNetCore
3- Hangfire.MySqlStorage

then you need to configure the hangfire in your startup class:
  - add the following code to the ConfigureServices method:
      
            var hangfireConnectionString = Configuration.GetConnectionString("HangfireDb");

            services.AddHangfire(configuration => configuration
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseStorage(
                    new MySqlStorage(
                        hangfireConnectionString,
                        new MySqlStorageOptions
                        {
                            QueuePollInterval = TimeSpan.FromSeconds(10),
                            JobExpirationCheckInterval = TimeSpan.FromHours(1),
                            CountersAggregateInterval = TimeSpan.FromMinutes(5),
                            PrepareSchemaIfNecessary = true,
                            DashboardJobListLimit = 25000,
                            TransactionTimeout = TimeSpan.FromMinutes(1),
                            TablesPrefix = "Hangfire",
                        }
                    )
                ));
                  
            // Add the processing server as IHostedService
            services.AddHangfireServer(options => options.WorkerCount = 1);
     
     
If you want to use the hangfire dashboard with authentication install this package Hangfire.Dashboard.Basic.Authentication
and add the following code to the Configure method in your startup class:

            app.UseHangfireDashboard("/hangfire", new DashboardOptions
            {
                // AppPath = "" //The path for the Back To Site link. Set to null in order to hide the Back To  Site link.
                DashboardTitle = "Hangfire",
                Authorization = new[]
                {
                    new HangfireCustomBasicAuthenticationFilter{
                        User = "mohamed@gmail.com",
                        Pass = "MohamedP@ssw0rd123"
                    }
                },
            });
