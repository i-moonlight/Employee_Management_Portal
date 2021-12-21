use actix_web::web;

use super::controller::{
    get_products,
    get_product_by_id,
    create_product,
    update_product,
    delete_product,
};

pub fn config(conf: &mut web::ServiceConfig) {
    let scope = web::scope("/api")
        .service(get_products)
        .service(get_product_by_id)
        .service(create_product)
        .service(update_product)
        .service(delete_product);
    conf.service(scope);
}
