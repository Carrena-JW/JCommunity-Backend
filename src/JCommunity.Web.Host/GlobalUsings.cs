global using JCommunity.AppCore.Core.Abstractions;
global using JCommunity.AppCore.Entities.TopicAggregate;
global using JCommunity.Infrastructure;
global using JCommunity.Infrastructure.Setup;
global using JCommunity.Services.Extentions;
global using JCommunity.Services.MemberService.Commands;
global using JCommunity.Services.MemberService.Queries;
global using JCommunity.Services.PostService.Commands;
global using JCommunity.Services.PostService.Queries;
global using JCommunity.Services.TopicService.Commands;
global using JCommunity.Web.Host.ApiEndpoints.File;
global using JCommunity.Web.Host.ApiEndpoints.Member;
global using JCommunity.Web.Host.ApiEndpoints.Post;
global using JCommunity.Web.Host.ApiEndpoints.Topic;
global using JCommunity.Web.Host.SeedWork;
global using JCommunity.Web.Host.SeedWork.ExceptionHandlers;
global using JCommunity.Web.Host.SeedWork.Filters;
global using MassTransit;
global using MediatR;
global using Microsoft.AspNetCore.Diagnostics;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.Extensions.Diagnostics.HealthChecks;
global using Serilog;
global using Serilog.Exceptions;
global using Serilog.Sinks.Elasticsearch;
global using System.Text;
global using System.Text.Json;
