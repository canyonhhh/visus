using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;

namespace visus.Tests;

public class WebTests
{
    private static readonly TimeSpan DefaultTimeout = TimeSpan.FromSeconds(30);
    
    [Fact]
    public void Pass()
    {
        Assert.True(true);
    }

}