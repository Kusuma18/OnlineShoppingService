using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using OnlineShoppingService.Models;

namespace OnlineShoppingService.Controllers
{
    public class MyCardsController : ApiController
    {
        private MyCardDBContext db = new MyCardDBContext();

        public IQueryable<MyCard> GetCards()
        {
            return db.Cards;
        }

        [ResponseType(typeof(MyCard))]
        public IHttpActionResult GetMyCard(int id)
        {
            MyCard myCard = db.Cards.Find(id);
            if (myCard == null)
            {
                return NotFound();
            }

            return Ok(myCard);
        }

        [ResponseType(typeof(void))]
        public IHttpActionResult PutMyCard(int id, MyCard myCard)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != myCard.CardID)
            {
                return BadRequest();
            }

            db.Entry(myCard).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MyCardExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        [ResponseType(typeof(MyCard))]
        public IHttpActionResult PostMyCard(MyCard myCard)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Cards.Add(myCard);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = myCard.CardID }, myCard);
        }

        [ResponseType(typeof(MyCard))]
        public IHttpActionResult DeleteMyCard(int id)
        {
            MyCard myCard = db.Cards.Find(id);
            if (myCard == null)
            {
                return NotFound();
            }

            db.Cards.Remove(myCard);
            db.SaveChanges();

            return Ok(myCard);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MyCardExists(int id)
        {
            return db.Cards.Count(e => e.CardID == id) > 0;
        }
    }
}