require 'rails_helper'

RSpec.describe "/businesses", type: :request do

  let(:valid_attributes) { { name: "Test Business", contact_info: "+37060000000" } }

  let(:invalid_attributes) { { name: nil, contact_info: "Invalid Contact" } }

  describe "GET /index" do
    it "renders a successful response" do
      Business.create! valid_attributes
      get businesses_url
      expect(response).to be_successful
    end
  end

  describe "GET /show" do
    it "renders a successful response" do
      business = Business.create! valid_attributes
      get business_url(business)
      expect(response).to be_successful
    end
  end

  describe "GET /new" do
    it "renders a successful response" do
      get new_business_url
      expect(response).to be_successful
    end
  end

  describe "GET /edit" do
    it "renders a successful response" do
      business = Business.create! valid_attributes
      get edit_business_url(business)
      expect(response).to be_successful
    end
  end

  describe "POST /create" do
    context "with valid parameters" do
      it "creates a new Business" do
        expect {
          post businesses_url, params: { business: valid_attributes }
        }.to change(Business, :count).by(1)
      end

      it "redirects to the created business" do
        post businesses_url, params: { business: valid_attributes }
        expect(response).to redirect_to(business_url(Business.last))
      end
    end

    context "with invalid parameters" do
      it "does not create a new Business" do
        expect {
          post businesses_url, params: { business: invalid_attributes }
        }.to change(Business, :count).by(0)
      end

      it "renders a response with 422 status (i.e. to display the 'new' template)" do
        post businesses_url, params: { business: invalid_attributes }
        expect(response).to have_http_status(:unprocessable_content)
      end
    end
  end

  describe "PATCH /update" do
    context "with valid parameters" do
      let(:new_attributes) {
        skip("Add a hash of attributes valid for your model")
      }

      it "updates the requested business" do
        business = Business.create! valid_attributes
        patch business_url(business), params: { business: new_attributes }
        business.reload
        skip("Add assertions for updated state")
      end

      it "redirects to the business" do
        business = Business.create! valid_attributes
        patch business_url(business), params: { business: new_attributes }
        business.reload
        expect(response).to redirect_to(business_url(business))
      end
    end

    context "with invalid parameters" do
      it "renders a response with 422 status (i.e. to display the 'edit' template)" do
        business = Business.create! valid_attributes
        patch business_url(business), params: { business: invalid_attributes }
        expect(response).to have_http_status(:unprocessable_content)
      end
    end
  end

  describe "DELETE /destroy" do
    it "destroys the requested business" do
      business = Business.create! valid_attributes
      expect {
        delete business_url(business)
      }.to change(Business, :count).by(-1)
    end

    it "redirects to the businesses list" do
      business = Business.create! valid_attributes
      delete business_url(business)
      expect(response).to redirect_to(businesses_url)
    end
  end
end
