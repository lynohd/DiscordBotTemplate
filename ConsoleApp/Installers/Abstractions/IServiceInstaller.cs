using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
public interface IServiceInstaller
{
    public void Install(IServiceCollection services, IConfiguration config);
}
