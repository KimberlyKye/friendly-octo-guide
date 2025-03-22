using Domain;

namespace Tests;

public class Tests
{
    DomainTestClass _domainTestClass;
    [SetUp]
    public void Setup()
    {
        _domainTestClass = new DomainTestClass();
    }

    [Test]
    public void Test1()
    {
        var expectedValue = true;

        var result = _domainTestClass.TestMethod();

        Assert.That(result, Is.EqualTo(expectedValue));
    }

}