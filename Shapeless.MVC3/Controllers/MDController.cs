using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Shapeless.Core;
using Shapeless.MVC3.Models;

namespace Shapeless.MVC3.Controllers
{
    public abstract class MDController <M> : Controller where M:class
    {

       
        
        protected abstract string title { get; }

       

        protected abstract M newM { get; }

        protected abstract bool isUndefined(string val);

        /// <summary>
        /// 插入前验证和处理
        /// </summary>
        /// <param name="m"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        protected virtual bool onCreate(ref M m, ref Result result) { return true; }

        /// <summary>
        /// 插入成功
        /// </summary>
        /// <param name="m"></param>
        /// <param name="result"></param>
        protected virtual void onCreateSuccess(ref M m, ref Result result)
        {
           
        }

       

        /// <summary>
        /// 更新前验证和处理
        /// </summary>
        /// <param name="m"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        protected virtual void onUpdate(ref M m, ref Result result)
        {
            
        }

        /// <summary>
        /// 更新成功
        /// </summary>
        /// <param name="m"></param>
        /// <param name="result"></param>
        protected virtual void onUpdateSuccess(ref M m, ref Result result)
        {
          
            result.attrs["model"] = m;
            return;
        }

        

        /// <summary>
        /// 删除前验证
        /// </summary>
        /// <param name="m"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        protected virtual void onDelete(ref M m, ref Result result)
        {
            
        }

        

        /// <summary>
        /// 删除成功事件
        /// </summary>
        /// <param name="m">模型</param>
        /// <param name="result">结果对象</param>
        protected virtual void onDeleteSuccess(ref M m, ref Result result)
        {
           
        }
        protected virtual void onCreateException(ref M m, ref Result result, Exception e)
        {
           
        }
        protected virtual void onUpdateException(ref M m, ref Result result, Exception e)
        {
         
        }
        protected virtual void onDeleteException(ref M m, ref Result result, Exception e)
        {
           
        }

        protected abstract void _create(ref M m, ref Result re);

        protected abstract void _update(ref M m, ref Result re);

        protected abstract void _delete(ref M m, ref Result re);

        protected abstract M _read(string pk);


        [HttpPost]
        public ActionResult update(string pk)
        {
            
            M m = null;
            Result result = new Result();


            

            try
            {

                m = _read(pk);
                Clazz.@from(m, Request.Params, "", "");
                onUpdate(ref m, ref result);
                _update(ref m,ref result);
                onUpdateSuccess(ref m, ref result);
                
            }
            catch (Exception ex)
            {
               
                result.code = Result.CODE_EXCEPTION;
                //onUpdateError(ref m, ref result, ex);
                onUpdateException(ref m, ref result, ex);
            }


            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult delete(string pk)
        {
            M m = null;
            Result result = new Result();

            try
            {
                m = _read(pk);
                onDelete(ref m, ref result);
                
                    //_table_m.DeleteOnSubmit(m);
                    _delete(ref m, ref result);
                    
                    
                    onDeleteSuccess(ref m, ref result);
               
            }
            catch (Exception ex)
            {
              
                result.code = Result.CODE_EXCEPTION;
                //onDeleteError(ref m, ref result , ex);
                onDeleteException(ref m, ref result, ex);
            }


            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult create()
        {
            
            M m = newM;
            Result result = new Result();
            

            try
            {
                onCreate(ref m, ref result);
                
                    _create(ref m, ref result);
                    
                    onCreateSuccess(ref m, ref result);
                    result.attrs["model"] = m;
               
            }
            catch (Exception ex)
            {
               
                result.code = Result.CODE_EXCEPTION;
                // onCreateError(ref m, ref result, ex);
                onCreateException(ref m, ref result, ex);
            }


            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public virtual ActionResult save(string pk)
        {
            if (isUndefined(pk)) return create();
            return update(pk);
        }

    }
}
