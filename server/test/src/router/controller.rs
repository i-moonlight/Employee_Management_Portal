use crate::AppState;

use crate::model::product::{
    ProductModel,
    CreateProductSchema,
    UpdateProductSchema,
};

use actix_web::{get, post, put, delete, web, HttpResponse, Responder};
use chrono::Utc;
use serde_json::json;


#[get("/products")] // http://localhost:8080/api/products
pub async fn get_products(state: web::Data<AppState>) -> impl Responder {
    let query_result = sqlx::query_as!(
        ProductModel,
        "SELECT * FROM products",
    )
        .fetch_all(&state.db)
        .await;

    if query_result.is_err() {
        let message: &str = "Something bad happened while fetching the products";
        return HttpResponse::InternalServerError().json(
            json!({
                "status": "error",
                "message": message
            })
        );
    }

    let products = query_result.unwrap();

    HttpResponse::Ok().json(
        json!({
            "status": 200,
            "no. products": products.len(),
            "products": products
        })
    )
}

#[post("/products/product")]
pub async fn create_product(product: web::Json<CreateProductSchema>, state: web::Data<AppState>) -> impl Responder {
    let query_result = sqlx::query_as!(
        ProductModel,
        "INSERT into products (brand, category, description, image, price, rating, num_reviews, count_in_stock)\
        VALUES ($1, $2, $3, $4, $5, $6, $7, $8) returning *",
        product.brand.to_string(),
        product.category.to_string(),
        product.description.to_string(),
        product.image.to_string(),
        product.price,
        product.rating,
        product.num_reviews,
        product.count_in_stock,
    )
        .fetch_one(&state.db)
        .await;

    match query_result {
        Ok(product) => {
            let product_response = serde_json::json!({
                "status": "success",
                "data": serde_json::json!({"product": product})
            });
            return HttpResponse::Ok().json(product_response);
        }
        Err(e) => {
            if e.to_string().contains("duplicate key value violates unique constraint") {
                return HttpResponse::BadRequest().json(serde_json::json!({
                    "status": "fail",
                    "message": "Duplicate Key"}
                ));
            }
            return HttpResponse::InternalServerError().json(serde_json::json!({
                "status": "error",
                "message": format!("{:?}", e)
            }));
        }
    }
}

#[get("/products/product/{id}")]
pub async fn get_product_by_id(path: web::Path<uuid::Uuid>, state: web::Data<AppState>) -> impl Responder {
    let product_id = path.into_inner();
    let query_result = sqlx::query_as!(
        ProductModel,
        "SELECT * FROM products WHERE id = $1",
        product_id
    )
        .fetch_one(&state.db)
        .await;

    match query_result {
        Ok(product) => {
            let product_response = serde_json::json!({
                "status": "success",
                "data": serde_json::json!({"product": product})
            });
            return HttpResponse::Ok().json(product_response);
        }
        Err(_) => {
            let message = format!("Game with ID: {} not found", product_id);
            return HttpResponse::NotFound().json(
                serde_json::json!({
                    "status": "fail",
                    "message": message
                })
            );
        }
    }
}

#[put("/products/product/{id}")]
pub async fn update_product(path: web::Path<uuid::Uuid>, state: web::Data<AppState>, body: web::Json<UpdateProductSchema>) -> impl Responder {
    let product_id = path.into_inner();

    let query_result = sqlx::query_as!(ProductModel, "SELECT * FROM products where id = $1", product_id)
        .fetch_one(&state.db)
        .await;

    if query_result.is_err() {
        let message = format!("Product with ID: {} not found", product_id);
        return HttpResponse::NotFound().json(serde_json::json!({
            "status": "fail",
            "message": message
        }));
    }

    let now = Utc::now();
    let product = query_result.unwrap();

    let query_result = sqlx::query_as!(
        ProductModel,
        "UPDATE products SET brand = $1, category = $2, description = $3, image = $4, price = $5,\
         rating = $6, num_reviews = $7, count_in_stock = $8, updated_at = $9 WHERE id = $10 returning *",
        body.brand.to_owned().unwrap_or(product.brand),
        body.category.to_owned().unwrap_or(product.category),
        body.description.to_owned().unwrap_or(product.description),
        body.image.to_owned().unwrap_or(product.image),
        body.price.to_owned(),
        body.rating.to_owned(),
        body.num_reviews.to_owned(),
        body.count_in_stock.to_owned(),
        now,
        product_id
    )
        .fetch_one(&state.db)
        .await;

    match query_result {
        Ok(product) => {
            let product_response = serde_json::json!({
                "state": "success",
                "data": serde_json::json!({ "product": product })
            });
            return HttpResponse::Ok().json(product_response);
        }
        Err(_) => {
            let message = format!("Product with ID: {} not found", product_id);
            return HttpResponse::NotFound().json(
                serde_json::json!({
                    "status": "fail",
                    "message": message
                })
            );
        }
    }
}

#[delete("/products/product/{id}")]
pub async fn delete_product(path: web::Path<uuid::Uuid>, state: web::Data<AppState>) -> impl Responder {
    let product_id = path.into_inner();

    let rows_affected = sqlx::query!("DELETE from products WHERE id = $1", product_id)
        .execute(&state.db)
        .await
        .unwrap()
        .rows_affected();

    if rows_affected == 0 {
        let message = format!("Product with ID: {} not found", product_id);
        return HttpResponse::NotFound().json(
            json!({
                "status": "fail",
                "message": message
            })
        );
    }
    HttpResponse::NoContent().finish()
}
