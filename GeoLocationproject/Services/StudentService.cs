using GeoLocationproject.Models;
using GeoLocationproject.Repositories;

public class StudentService
{
    private readonly IStudentRepository _repo;
    public StudentService(IStudentRepository repo)
    {
        _repo = repo;
    }

    public Task<List<Student>> GetAllAsync() => _repo.GetAllAsync();
    public Task<Student?> GetByIdAsync(int id) => _repo.GetByIdAsync(id);
    public Task<Student> AddAsync(Student student) => _repo.AddAsync(student);
    public Task UpdateAsync(Student student) => _repo.UpdateAsync(student);
    public Task DeleteAsync(int id) => _repo.DeleteAsync(id);
}
