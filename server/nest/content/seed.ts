import { PrismaClient, Product } from '@prisma/client'
import { faker } from '@faker-js/faker';
import * as dotenv from 'dotenv';

dotenv.config();
const prisma = new PrismaClient();

const createProducts = async (quantity: number) => {
  const products: Product[] = [];
  for (let i = 0; i < quantity; i++) {
    const productName = faker.commerce.productName();
    const categoryName = faker.commerce.department();

    const product = await prisma.product.create({
      data: {
        name: productName,
        slug: faker.helpers.slugify(productName).toLowerCase(),
        description: faker.commerce.productDescription(),
        price: +faker.commerce.price({ min: 100, max: 200 }),
        images: Array.from({length: 4}).map(() => faker.image.urlLoremFlickr({ height: 250 })),
        category: {
          create: {
            name: categoryName,
            slug: faker.helpers.slugify(categoryName).toLowerCase()
          }
        },
        reviews: {
          create: [
            {
              rating: faker.number.int({ min: 1, max: 5 }),
              text: faker.lorem.paragraph(),
              user: {connect: {id: '08a37c38-8baf-4e48-9e21-8a196b99236b'}}
            }
          ]
        }
      }
    });
    products.push(product);
  }
  console.log(`Created ${products.length} products!`);
}

async function main() {
  console.log('Start seeding');
  await createProducts(10);
}

main()
   .catch(e => console.log(e))
   .finally(async () => {
     await prisma.$disconnect()
   })
