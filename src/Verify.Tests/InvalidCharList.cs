using System.IO;
using System.Linq;
using System.Threading.Tasks;
using VerifyXunit;
using Xunit;

[UsesVerify]
public class InvalidCharList
{
    [Fact]
    public Task Foo()
    {
        var array = Path.GetInvalidPathChars()
            .Concat(Path.GetInvalidFileNameChars())
            .Distinct()
            .ToArray();
        return Verifier.Verify(array);
    }
}