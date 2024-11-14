using CloudinaryDotNet;

namespace Backend_Teamwork.src.Utils;

public static class CloudinaryConfig
{
    public static void AddCloudinary(this IServiceCollection services, IConfiguration configuration)
    {
        var cloudinaryConfig = configuration.GetSection("Cloudinary");
        var account = new Account(
            cloudinaryConfig["CloudName"],
            cloudinaryConfig["ApiKey"],
            cloudinaryConfig["ApiSecret"]
        );

        Cloudinary cloudinary = new Cloudinary(account);
        services.AddSingleton(cloudinary);
    }
}
