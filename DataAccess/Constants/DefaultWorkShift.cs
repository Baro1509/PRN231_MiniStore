using DataAccess.Models;

namespace DataAccess.Constants
{
    public class DefaultWorkShift
    {
        public static WorkShift DefaultShift1(DateOnly date, string managerId)
        {
            WorkShift ws = new WorkShift();
            ws.Bonus = 0;
            ws.Coefficient = 1;
            ws.CreatedBy = managerId;
            ws.StartTime = date.ToDateTime(new TimeOnly(6, 0));
            ws.EndTime = date.ToDateTime(new TimeOnly(10, 0));
            ws.Status = (byte?)Status.Available;
            return ws;
        }
        public static WorkShift DefaultShift2(DateOnly date, string managerId)
        {
            WorkShift ws = new WorkShift();
            ws.Bonus = 0;
            ws.Coefficient = 1;
            ws.CreatedBy = managerId;
            ws.StartTime = date.ToDateTime(new TimeOnly(8, 0));
            ws.EndTime = date.ToDateTime(new TimeOnly(12, 0));
            ws.Status = (byte?)Status.Available;
            return ws;
        }
        public static WorkShift DefaultShift3(DateOnly date, string managerId)
        {
            WorkShift ws = new WorkShift();
            ws.Bonus = 0;
            ws.Coefficient = 1;
            ws.CreatedBy = managerId;
            ws.StartTime = date.ToDateTime(new TimeOnly(10, 0));
            ws.EndTime = date.ToDateTime(new TimeOnly(14, 0));
            ws.Status = (byte?)Status.Available;
            return ws;
        }
        public static WorkShift DefaultShift4(DateOnly date, string managerId)
        {
            WorkShift ws = new WorkShift();
            ws.Bonus = 0;
            ws.Coefficient = 1;
            ws.CreatedBy = managerId;
            ws.StartTime = date.ToDateTime(new TimeOnly(14, 0));
            ws.EndTime = date.ToDateTime(new TimeOnly(18, 0));
            ws.Status = (byte?)Status.Available;
            return ws;
        }
        public static WorkShift DefaultShift5(DateOnly date, string managerId)
        {
            WorkShift ws = new WorkShift();
            ws.Bonus = 0;
            ws.Coefficient = 1;
            ws.CreatedBy = managerId;
            ws.StartTime = date.ToDateTime(new TimeOnly(18, 0));
            ws.EndTime = date.ToDateTime(new TimeOnly(22, 0));
            ws.Status = (byte?)Status.Available;
            return ws;
        }
        public static WorkShift DefaultShift6(DateOnly date, string managerId)
        {
            WorkShift ws = new WorkShift();
            ws.Bonus = 0;
            ws.Coefficient = 2;
            ws.CreatedBy = managerId;
            ws.StartTime = date.ToDateTime(new TimeOnly(22, 0));
            ws.EndTime = date.AddDays(1).ToDateTime(new TimeOnly(6, 0));
            ws.Status = (byte?)Status.Available;
            return ws;
        }
    }
}
