using BenchmarkDotNet.Attributes;
using Bogus;

namespace LinqVsLambdaPerformanceBenchmark;

[MemoryDiagnoser(false)]
public class QueryBenchmark
{
    private readonly List<User> _users = new();

    [GlobalSetup]
    public void GlobalSetup()
    {
        var userFaker = new Faker<User>()
            .RuleFor(x => x.Id, f => Guid.NewGuid())
            .RuleFor(x => x.Name, f => f.Person.FirstName)
            .RuleFor(x => x.Surname, f => f.Person.LastName)
            .RuleFor(x => x.FullName, f => f.Person.FullName)
            .RuleFor(x => x.EmailAddress, f => f.Person.Email)
            .RuleFor(x => x.MobilePhoneNumber, f => f.Person.Phone)
            .RuleFor(x => x.Age, f => f.Random.Number(5, 35))
            .Generate(25);

        _users.AddRange(userFaker);
    }

    #region Where

    [Benchmark]
    public void Linq_Where()
    {
        var query = from user in _users
                    where user.Age > 18
                    select user;

        var result = query.ToList();
    }

    [Benchmark]
    public void Lambda_Where()
    {
        var result = _users.Where(x => x.Age > 18).ToList();
    }

    #endregion

    #region Any

    [Benchmark]
    public void Linq_Any()
    {
        var query = from user in _users
                    where user.Age > 18
                    select user;

        var result = query.Any();
    }

    [Benchmark]
    public void Lambda_Where_Any()
    {
        var result = _users.Where(x => x.Age > 18).Any();
    }

    [Benchmark]
    public void Lambda_Any()
    {
        var result = _users.Any(x => x.Age > 18);
    }

    #endregion

    #region First

    [Benchmark]
    public void Linq_First()
    {
        var query = from user in _users
                    where user.Age > 18
                    select user;

        var result = query.First();
    }

    [Benchmark]
    public void Lambda_Where_First()
    {
        var result = _users.Where(x => x.Age > 18).First();
    }

    [Benchmark]
    public void Lambda_First()
    {
        var result = _users.First(x => x.Age > 18);
    }

    #endregion

    #region FirstOrDefault

    [Benchmark]
    public void Linq_FirstOrDefault()
    {
        var query = from user in _users
                    where user.Age > 18
                    select user;

        var result = query.FirstOrDefault();
    }

    [Benchmark]
    public void Lambda_Where_FirstOrDefault()
    {
        var result = _users.Where(x => x.Age > 18).FirstOrDefault();
    }

    [Benchmark]
    public void Lambda_FirstOrDefault()
    {
        var result = _users.FirstOrDefault(x => x.Age > 18);
    }

    #endregion

    #region Last

    [Benchmark]
    public void Linq_Last()
    {
        var query = from user in _users
                    where user.Age > 18
                    select user;

        var result = query.Last();
    }

    [Benchmark]
    public void Lambda_Where_Last()
    {
        var result = _users.Where(x => x.Age > 18).Last();
    }

    [Benchmark]
    public void Lambda_Last()
    {
        var result = _users.Last(x => x.Age > 18);
    }

    #endregion

    #region LastOrDefault

    [Benchmark]
    public void Linq_LastOrDefault()
    {
        var query = from user in _users
                    where user.Age > 18
                    select user;

        var result = query.LastOrDefault();
    }

    [Benchmark]
    public void Lambda_Where_LastOrDefault()
    {
        var result = _users.Where(x => x.Age > 18).LastOrDefault();
    }

    [Benchmark]
    public void Lambda_LastOrDefault()
    {
        var result = _users.LastOrDefault(x => x.Age > 18);
    }

    #endregion
}
