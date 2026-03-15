using Serilog;
using StockManager.Api.Middleware;

namespace StockManagerApi.Configuration
{
    public static class WebConfigureWebApplication
    {
        public static WebApplication ConfigureWebApplication(this WebApplication app)
        {
            app.ConfigureSwagger()
               .ConfigureRedirection()
               .ConfigureCors()
               .ConfigureAuthorization()
               .ConfigureControllers()
               .ConfigureLogger()
               .ConfigureMiddleware();

            return app;
        }


        private static WebApplication ConfigureLogger(this WebApplication app)
        {
            app.UseSerilogRequestLogging();
            return app;
        }

        private static WebApplication ConfigureSwagger(this WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            return app;
        }
        private static WebApplication ConfigureRedirection(this WebApplication app)
        {
            app.UseHttpsRedirection();
            return app;
        }
        private static WebApplication ConfigureAuthorization(this WebApplication app)
        {
            app.UseAuthentication();
            app.UseAuthorization();
            return app;
        }

        private static WebApplication ConfigureMiddleware(this WebApplication app)
        {
            app.UseMiddleware<ExceptionHandlingMiddleware>();
            return app;
        }
        private static WebApplication ConfigureControllers(this WebApplication app)
        {
            app.MapControllers();
            return app;
        }
        private static WebApplication ConfigureCors(this WebApplication app)
        {
            app.UseCors("AllowAll");
            return app;
        }
    }
}
