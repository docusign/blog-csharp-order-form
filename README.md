# Blog: Sending an order form including credit card payment  

Repository: [blog-csharp-order-form](https://github.com/docusign/blog-csharp-order-form)

This C# .NET Core code example demonstrates how to send an
order form to a purchaser using DocuSign.

The purchaser uses DocuSign both to legally agree to the
purchase contract and to pay for the purchase using a credit
card.

* A [4 minute video](https://docusigninc.box.com/s/013ktszmj5bphyiukypmknz250wmu5zd)
  demonstrates the example.
* This example is also discussed in a post on the
  DocuSign Developer Blog.

## Installation and Configuration

Follow the INSTALLATION.md file's instructions

## Running the example

Build, then run the solution to send the order form,
along with its signature request and payment form,
to the purchaser via email.

The purchaser's name and email is set using the App.config file.

After the purchaser agrees to the order, they'll be asked to pay.

If you're testing with the Stripe payment service, you can use
one of their test credit card numbers.

For example: `4242 4242 4242 4242` 

Additional
[test card numbers](https://stripe.com/docs/testing#cards)
are also available.

## Support, Contributions, License

Submit support questions to [StackOverflow](https://stackoverflow.com). Use tag `docusignapi`.

Contributions via Pull Requests are appreciated.
All contributions must use the MIT License.

This repository uses the MIT license, see the
LICENSE file.
