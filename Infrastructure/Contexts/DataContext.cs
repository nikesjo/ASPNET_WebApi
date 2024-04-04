using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Contexts;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    public virtual DbSet<CategoryEntity> Categories { get; set; }
    public virtual DbSet<ContactEntity> Contacts { get; set; }
    public virtual DbSet<CourseEntity> Courses { get; set; }
    public virtual DbSet<SubscriberEntity> Subscribers { get; set; }
}
