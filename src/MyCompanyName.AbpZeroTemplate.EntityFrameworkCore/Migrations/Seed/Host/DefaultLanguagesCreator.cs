using System.Collections.Generic;
using System.Linq;
using Abp.Localization;
using Microsoft.EntityFrameworkCore;
using MyCompanyName.AbpZeroTemplate.EntityFrameworkCore;

namespace MyCompanyName.AbpZeroTemplate.Migrations.Seed.Host
{
    public class DefaultLanguagesCreator
    {
        public static List<ApplicationLanguage> InitialLanguages => GetInitialLanguages();

        private readonly AbpZeroTemplateDbContext _context;

        private static List<ApplicationLanguage> GetInitialLanguages()
        {
            var tenantId = AbpZeroTemplateConsts.MultiTenancyEnabled ? null : (int?)1;
            return new List<ApplicationLanguage>
            {
                new ApplicationLanguage(tenantId, "en", "English", "flag-icon flag-icon-gb"),
                new ApplicationLanguage(tenantId, "zh-CN", "简体中文", "flag-icon flag-icon-cn"),
                new ApplicationLanguage(tenantId, "de", "Deutsch", "flag-icon flag-icon-de"),
                new ApplicationLanguage(tenantId, "fr", "Français", "flag-icon flag-icon-fr"),
                new ApplicationLanguage(tenantId, "it", "Italiano", "flag-icon flag-icon-it"),
                new ApplicationLanguage(tenantId, "ar", "العربية", "flag-icon flag-icon-sa"),
                new ApplicationLanguage(tenantId, "pt-BR", "Português (Brasil)", "flag-icon flag-icon-br"),
                new ApplicationLanguage(tenantId, "tr", "Türkçe", "flag-icon flag-icon-tr"),
                new ApplicationLanguage(tenantId, "ru", "Pусский", "flag-icon flag-icon-ru"),
                new ApplicationLanguage(tenantId, "es-MX", "Español (México)", "flag-icon flag-icon-mx"),
                new ApplicationLanguage(tenantId, "es", "Español (Spanish)", "flag-icon flag-icon-es")
            };
        }

        public DefaultLanguagesCreator(AbpZeroTemplateDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            CreateLanguages();
        }

        private void CreateLanguages()
        {
            foreach (var language in InitialLanguages)
            {
                AddLanguageIfNotExists(language);
            }
        }

        private void AddLanguageIfNotExists(ApplicationLanguage language)
        {
            if (_context.Languages.IgnoreQueryFilters().Any(l => l.TenantId == language.TenantId && l.Name == language.Name))
            {
                return;
            }

            _context.Languages.Add(language);

            _context.SaveChanges();
        }
    }
}