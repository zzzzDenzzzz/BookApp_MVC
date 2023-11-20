using WebApplication1.Models;

namespace WebApplication1.Extensions;

public static class DbContextExtensions
{
    public static void UpdateManyToMany<T, TKey>(this BookDbContext db, IEnumerable<T>? currentItems,
        IEnumerable<T> newItems, Func<T, TKey> getKey) where T : class
    {
        if (currentItems != null)
        {
            db.Set<T>().RemoveRange(currentItems.Except(newItems, getKey));
            db.Set<T>().AddRange(newItems.Except(currentItems, getKey));
        }
        else
        {
            db.Set<T>().AddRange(newItems);
        }
    }


    public static IEnumerable<T> Except<T, TKey>(this IEnumerable<T> items, IEnumerable<T> other,
        Func<T, TKey> getKeyFunc)
    {
        return items.GroupJoin(other, getKeyFunc, getKeyFunc, (item, templateItems) => new { item, templateItems })
            .SelectMany(t => t.templateItems.DefaultIfEmpty(), (t, tmp) => new { t, tmp })
            .Where(t => ReferenceEquals(null, t.tmp) || t.tmp.Equals(default(T)))
            .Select(t => t.t.item);
    }
}