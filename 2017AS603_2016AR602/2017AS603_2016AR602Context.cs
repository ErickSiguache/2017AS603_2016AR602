using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using _2017AS603_2016AR602.Models;

namespace _2017AS603_2016AR602
{
    public class _2017AS603_2016AR602Context : DbContext
    {
        public _2017AS603_2016AR602Context(DbContextOptions<_2017AS603_2016AR602Context> options) : base(options)
        {
        }
    }
}
