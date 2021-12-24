use bigdecimal::BigDecimal;
use serde::{Deserialize, Serialize};
use sqlx::FromRow;
use uuid::Uuid;


#[derive(Debug, FromRow, Deserialize, Serialize)]
#[allow(non_snake_case)]
pub struct ProductModel {
    pub id:             Uuid,
    pub brand:          String,
    pub category:       String,
    pub description:    String,
    pub image:          String,
    pub price:          Option<BigDecimal>,
    pub rating:         Option<f32>,
    pub num_reviews:    Option<i32>,
    pub count_in_stock: Option<i32>,

    #[serde(rename = "createdAt")]
    pub created_at: Option<chrono::DateTime<chrono::Utc>>,

    #[serde(rename = "updatedAt")]
    pub updated_at: Option<chrono::DateTime<chrono::Utc>>,
}

#[derive(Serialize, Deserialize, Debug)]
pub struct CreateProductSchema {
    pub brand:          String,
    pub category:       String,
    pub description:    String,
    pub image:          String,
    pub price:          Option<BigDecimal>,
    pub rating:         Option<f32>,
    pub num_reviews:    Option<i32>,
    pub count_in_stock: Option<i32>,
}

#[derive(Serialize, Deserialize, Debug)]
pub struct UpdateProductSchema {
    pub brand:          Option<String>,
    pub category:       Option<String>,
    pub description:    Option<String>,
    pub image:          Option<String>,
    pub price:          Option<BigDecimal>,
    pub rating:         Option<f32>,
    pub num_reviews:    Option<i32>,
    pub count_in_stock: Option<i32>,
}
