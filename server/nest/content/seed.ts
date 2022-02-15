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
        slug: faker.helpers.slugify(productName),
        description: faker.commerce.productDescription(),
        price: +faker.commerce.price({ min: 100, max: 200 }),
        images: Array.from({length: 4}).map(() => faker.image.avatar()),
        category: {
          create: {
            name: categoryName,
            slug: faker.helpers.slugify(categoryName)
          }
        },
        reviews: {
          create: [
            {
              rating: faker.number.int({ min: 1, max: 5 }),
              text: faker.lorem.paragraph(),
              // user: {connect: {id: 'c54d4628-e709-489e-8302-299d3f269791'}}
            },
            {
              rating: faker.number.int({ min: 1, max: 5 }),
              text: faker.lorem.paragraph(),
            },
            {
              rating: faker.number.int({ min: 1, max: 5 }),
              text: faker.lorem.paragraph(),
            },
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
