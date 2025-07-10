using AutoMapper;
using SmartBookingSystem.Application.DTOs.Appointment;
using SmartBookingSystem.Application.DTOs.Customer;
using SmartBookingSystem.Application.DTOs.Provider;
using SmartBookingSystem.Application.DTOs.ProviderPost;
using SmartBookingSystem.Application.DTOs.Service;
using SmartBookingSystem.Application.DTOs.ServiceCategory;
using SmartBookingSystem.Application.DTOs.WeeklySchedule;
using SmartBookingSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartBookingSystem.Application.Mapping
{
    public class MappingProfile : Profile

    {
        public MappingProfile()
        {
            CreateMap<ProviderRequest, Provider>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.ApplicationUserId, opt => opt.Ignore());

            CreateMap<Provider, ProviderResponse>()
                .ForMember(dest => dest.ServiceCategoryName, opt => opt.MapFrom(src => src.ServiceCategory.Name));
            CreateMap<Provider, ProviderListItemResponse>()
                .ForMember(dest => dest.ServiceCategoryName, opt => opt.MapFrom(src => src.ServiceCategory.Name));

            CreateMap<Customer, CustomerResponse>();
            CreateMap<CustomerRequest, Customer>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.ApplicationUserId, opt => opt.Ignore());

            CreateMap<ServiceCategoryRequest, ServiceCategory>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<ServiceCategory, ServiceCategoryResponse>();
            CreateMap<ServiceCategory, ServiceCategoryWithProviderResponse>();

            CreateMap<Service, ServiceResponse>()
                .ForMember(dest => dest.ProviderName, opt => opt.MapFrom(src => $"{src.Provider.FirstName} {src.Provider.LastName}"))
                .ForMember(dest => dest.ServiceCategoryName, opt => opt.MapFrom(src => src.ServiceCategory.Name));
            CreateMap<ServiceRequest, Service>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.ProviderId, opt => opt.Ignore());

            CreateMap<WeeklyScheduleRequest, WeeklySchedule>()
                .ForMember(dest => dest.ProviderId, opt => opt.Ignore());

            CreateMap<WeeklySchedule, WeeklyScheduleResponse>()
                .ForMember(dest => dest.ProviderName, opt => opt.MapFrom(src => $"{src.Provider.FirstName} {src.Provider.LastName}"))
                .ForMember(dest => dest.StartTime, opt => opt.MapFrom(src => DateTime.Today.Add(src.StartTime).ToString("hh:mm tt")))
                .ForMember(dest => dest.EndTime, opt => opt.MapFrom(src => DateTime.Today.Add(src.EndTime).ToString("hh:mm tt")))
                .ForMember(dest => dest.DayOfWeek, opt => opt.MapFrom(src => src.DayOfWeek.ToString()));

            CreateMap<Appointment, AppointmentResponse>()
                .ForMember(dest => dest.ProviderName, opt => opt.MapFrom(src => $"{src.Provider.FirstName} {src.Provider.LastName}"))
                .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => $"{src.Customer.FirstName} {src.Customer.LastName}"))
                .ForMember(dest => dest.ServiceName, opt => opt.MapFrom(src => src.Service.Name))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));

            CreateMap<AppointmentRequest, Appointment>();


            CreateMap<ProviderPostRequest, ProviderPost>()
                .ForMember(dest => dest.Images, opt => opt.Ignore());
            CreateMap<ProviderPost, ProviderPostResponse>()
                .ForMember(dest => dest.ImageUrls, opt => opt.MapFrom(src => src.Images.Select(i => i.ImageUrl)))
                .ForMember(dest => dest.ProviderName, opt => opt.MapFrom(src => $"{src.Provider.FirstName} {src.Provider.LastName}"));
                



        }
    }
}
