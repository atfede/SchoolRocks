using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Wispero.Data;
using Wispero.Entities;
using Wispero.Web.Helpers;
using Wispero.Web.Models;

namespace Wispero.Web.Controllers
{
    [NoCache]
    public class HomeController : Controller
    {
        Core.Services.IDataService<KnowledgeBaseItem> KnowledgeBaseData;
        Core.Services.IQueryService<KnowledgeBaseItem> KnowledgeBaseQuery;

        public HomeController(Core.Services.IDataService<KnowledgeBaseItem> dataService, Core.Services.IQueryService<KnowledgeBaseItem> queryService)
        {
            KnowledgeBaseData = dataService;
            KnowledgeBaseQuery = queryService;

            //TODO: Implement mapping from QuestionAndAnswerModel to Entities.KnowledgeBaseItem.
            //LastUpdateOn field is set with DateTime.Now and Tags field with lowercase.
            //Also create a map from TagItem to TagModel.

            //TODO: Implement mapping as needed.
            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Entities.KnowledgeBaseItem, QuestionAndAnswerModel>();
                cfg.CreateMap<Entities.TagItem, TagModel>();
            });

        }

        public ActionResult Index()
        {
            return View();
        }

        [ChildActionOnly]
        public ActionResult Entry()
        {
            //TODO: Return partial view "Entry";

            return PartialView();
            
        }

        [ChildActionOnly]
        [HttpGet]
        public ActionResult TagCloud()
        {
            //TODO: Return partial view "TagCloud" with an instance of TagCloudviewModel.
            //You need to call TagHelper.Process as shown below.
            int tagMaxCount;
            var tagCloud = TagHelper.Process(KnowledgeBaseQuery, out tagMaxCount);
            
            throw new NotImplementedException();
           
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult New(QuestionAndAnswerModel model)
        {
            //TODO: Return partial view "Entry" with an instance of QuestionAndAnswerModel.
            //If model is valid then persists the new entry on DB. Make sure  data changes are committed.

            try
            {
                if (ModelState.IsValid)
                {
                    //QuestionAndAnswerModel qAA = AutoMapper.Mapper.Map<QuestionAndAnswerModel>(this.KnowledgeBaseQuery.Get(downCast.Id));

                    KnowledgeBaseItem newItem = new KnowledgeBaseItem
                    {
                        Query = model.Question,
                        Answer = model.Answer,
                        LastUpdateOn = DateTime.Now,
                        Tags = model.Tags
                    };
                    this.KnowledgeBaseData.Add(newItem);
                    this.KnowledgeBaseData.CommitChanges();
                }

                return PartialView(model);
            }
            catch {
                throw new Exception();
            }
        }
    }
}