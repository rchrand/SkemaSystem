using SkemaSystem.Migrations;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Web;
using System.Data.Entity.Infrastructure;

namespace SkemaSystem.Models
{
    // Do not remove or rename this class without premission!
    public interface ISkemaSystemDb : IDisposable
    {
        IDbSet<Teacher> Teachers { get; set; }
        IDbSet<ClassModel> Classes { get; set; }
        IDbSet<Education> Educations { get; set; }
        int SaveChanges();
        //DbEntityEntry Entry(object entity);
        void StateModified(object entity);
    }

    public class SkeamSystemDb : DbContext
    {
        public SkeamSystemDb() : base("name=skeamsysdb")
        {
            
        }

        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Education> Educations { get; set; }
        public DbSet<ClassModel> Classes { get; set; }

        //public void StateModified(object entity) 
        //{
        //    this.Entry(entity).State = System.Data.Entity.EntityState.Modified;
        //}
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<SkeamSystemDb, Configuration>());

        }
        
    }

    public class FakeSkemaSystemDb : ISkemaSystemDb
    {
        public IDbSet<ClassModel> Classes { get; set; }
        public IDbSet<Education> Educations { get; set; }
        public IDbSet<Teacher> Teachers { get; set; }
        public void Dispose() { }
        public int SaveChanges(){
            //this.Educations.Local = ()this.Educations.Local;
            return -1;
        }

        public void StateModified(object entity)
        {

        }

        public FakeSkemaSystemDb()
        {
            Teachers = new FakeDbSet<Teacher>();
            Educations = new FakeDbSet<Education>();
            Classes = new FakeDbSet<ClassModel>();
        }
    }

    public class FakeDbSet<T> : IDbSet<T> where T : class
    {
        private readonly HashSet<T> _data;
        private readonly IQueryable _query;
        private int _identity = 1;
        private List<PropertyInfo> _keyProperties;

        public FakeDbSet(IEnumerable<T> startData = null)
        {
            GetKeyProperties();
            _data = (startData != null ? new HashSet<T>(startData) : new HashSet<T>());
            _query = _data.AsQueryable();
        }

        private void GetKeyProperties()
        {
            _keyProperties = new List<PropertyInfo>();
            PropertyInfo[] properties = typeof(T).GetProperties();
            foreach (PropertyInfo property in properties)
            {
                foreach (Attribute attribute in property.GetCustomAttributes(true))
                {
                    if (attribute is KeyAttribute)
                    {
                        _keyProperties.Add(property);
                    }
                }
            }
        }

       

        private void GenerateId(T entity)
        {
            // If non-composite integer key
            if (_keyProperties.Count == 1 && _keyProperties[0].PropertyType == typeof(Int32))
                _keyProperties[0].SetValue(entity, _identity++, null);
        }

        public virtual T Find(params object[] keyValues)
        {
            if (keyValues.Length != _keyProperties.Count)
                throw new ArgumentException("Incorrect number of keys passed to find method");

            IQueryable<T> keyQuery = this.AsQueryable<T>();
            for (int i = 0; i < keyValues.Length; i++)
            {
                var x = i; // nested linq
                keyQuery = keyQuery.Where(entity => _keyProperties[x].GetValue(entity, null).Equals(keyValues[x]));
            }

            return keyQuery.SingleOrDefault();
        }

        public T Add(T item)
        {
            GenerateId(item);
            _data.Add(item);
            return item;
        }

        public T Remove(T item)
        {
            _data.Remove(item);
            return item;
        }

        public T Attach(T item)
        {
            _data.Add(item);
            return item;
        }

        public void Detach(T item)
        {
            _data.Remove(item);
        }

        Type IQueryable.ElementType
        {
            get { return _query.ElementType; }
        }

        Expression IQueryable.Expression
        {
            get { return _query.Expression; }
        }

        IQueryProvider IQueryable.Provider
        {
            get { return _query.Provider; }
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _data.GetEnumerator();
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return _data.GetEnumerator();
        }

        public T Create()
        {
            return Activator.CreateInstance<T>();
        }

        public ObservableCollection<T> Local
        {
            get
            {
                return new ObservableCollection<T>(_data);
            }
        }

        public TDerivedEntity Create<TDerivedEntity>() where TDerivedEntity : class, T
        {
            return Activator.CreateInstance<TDerivedEntity>();
        }
    }
}